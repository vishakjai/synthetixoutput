package main

import (
	"log"
	"net/http"
	"os"

	"github.com/go-chi/chi/v5"
	"github.com/jmoiron/sqlx"
	_ "github.com/lib/pq"
	"sort"
	"strings"
)

func main() {
	port := getPort()
	db, err := sqlx.Connect("postgres", os.Getenv("DATABASE_URL"))
	if err != nil {
		log.Fatalf("Failed to connect to database: %v", err)
	}
	r := chi.NewRouter()

	// Health endpoints
	r.Get("/health", healthHandler)
	r.Get("/ready", readyHandler)

	// Chat endpoints
	r.Get("/api/chat/{chatId}/messages", getChatMessagesHandler(db))
	r.Get("/api/chat/{chatId}", getChatHandler(db))
	r.Get("/api/chat", listChatsHandler(db))
	r.Post("/api/chat", createChatHandler(db))
	r.Post("/api/chat/{chatId}/add", addParticipantHandler(db))
	r.Delete("/api/chat/{chatId}/leave", leaveChatHandler(db))

	// Messages endpoints
	r.Delete("/api/messages/{messageId}", deleteMessageHandler(db))

	// Participants endpoints
	r.Get("/api/participants", listParticipantsHandler(db))
	r.Post("/api/participants/profile-picture-upload", uploadProfilePictureHandler(db))
	r.Post("/api/participants/confirm-profile-picture", confirmProfilePictureHandler(db))
	r.Delete("/api/participants/profile-picture", deleteProfilePictureHandler(db))

	log.Printf("Starting server on port %s", port)
	if err := http.ListenAndServe(":"+port, r); err != nil {
		log.Fatalf("Failed to start server: %v", err)
	}
}

func getPort() string {
	port := os.Getenv("PORT")
	if port == "" {
		port = "8080"
	}
	return port
}

// applyMigrations runs the bundled SQL files at startup. Doctor-injected;
// idempotent because each statement uses CREATE TABLE IF NOT EXISTS.
func applyMigrations(db *sqlx.DB) {
	entries, err := os.ReadDir("migrations")
	if err != nil {
		log.Printf("migrations dir missing: %v (skipping)", err)
		return
	}
	names := make([]string, 0, len(entries))
	for _, e := range entries {
		if !e.IsDir() && strings.HasSuffix(e.Name(), ".sql") {
			names = append(names, e.Name())
		}
	}
	sort.Strings(names)
	for _, name := range names {
		data, err := os.ReadFile("migrations/" + name)
		if err != nil {
			log.Printf("migration %s read failed: %v", name, err)
			continue
		}
		if _, err := db.Exec(string(data)); err != nil {
			log.Printf("migration %s exec failed (continuing): %v", name, err)
		}
	}
}

