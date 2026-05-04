package main

import (
	"log"
	"net/http"
	"os"

	"github.com/go-chi/chi/v5"
	"github.com/go-chi/chi/v5/middleware"
)

func main() {
	r := chi.NewRouter()
	r.Use(middleware.Logger)

	r.Get("/health", healthHandler)
	r.Get("/ready", readyHandler)

	r.Route("/api/chat", func(r chi.Router) {
		r.Get("/{chatId}/messages", getChatMessagesHandler)
		r.Get("/{chatId}", getChatHandler)
		r.Get("/", listChatsHandler)
		r.Post("/", createChatHandler)
		r.Post("/{chatId}/add", addParticipantHandler)
		r.Delete("/{chatId}/leave", leaveChatHandler)
	})

	port := os.Getenv("PORT")
	if port == "" {
		port = "8080"
	}

	log.Printf("Starting server on :%s", port)
	http.ListenAndServe(":"+port, r)
}

func healthHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status": "healthy"}`))
}

func readyHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status": "ready"}`))
}
