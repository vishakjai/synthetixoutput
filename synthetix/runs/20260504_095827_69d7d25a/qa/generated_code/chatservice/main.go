package main

import (
	"log"
	"net/http"
	"os"

	"github.com/go-chi/chi/v5"
)

func main() {
	r := chi.NewRouter()

	r.Get("/health", healthHandler)
	r.Get("/ready", readyHandler)

	r.Get("/api/chat/{chatId}/messages", getChatMessagesHandler)
	r.Get("/api/chat/{chatId}", getChatHandler)
	r.Get("/api/chat", listChatsHandler)
	r.Post("/api/chat", createChatHandler)
	r.Post("/api/chat/{chatId}/add", addParticipantHandler)
	r.Delete("/api/chat/{chatId}/leave", leaveChatHandler)

	port := os.Getenv("PORT")
	if port == "" {
		port = "8080"
	}

	log.Printf("Starting server on port %s", port)
	if err := http.ListenAndServe(":"+port, r); err != nil {
		log.Fatalf("Server failed to start: %v", err)
	}
}

func healthHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status": "healthy"}`))
}

func readyHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status": "ready"}`))
}
