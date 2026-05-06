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
	r := chi.NewRouter()

	// Health endpoints
	r.Get("/health", func(w http.ResponseWriter, r *http.Request) {
		w.WriteHeader(http.StatusOK)
		w.Write([]byte(`{"status": "healthy"}`))
	})

	r.Get("/ready", func(w http.ResponseWriter, r *http.Request) {
		w.WriteHeader(http.StatusOK)
		w.Write([]byte(`{"status": "ready"}`))
	})

	// ChatService endpoints
	registerChatRoutes(r)

	port := os.Getenv("PORT")
	if port == "" {
		port = "8080"
	}

	db, err := sqlx.Connect("postgres", os.Getenv("DATABASE_URL"))
	if err != nil {
		log.Fatalf("Failed to connect to the database: %v", err)
	}
	defer db.Close()

	applyMigrations(db)
	log.Printf("Starting server on port %s...", port)
	log.Fatal(http.ListenAndServe(":"+port, r))
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

