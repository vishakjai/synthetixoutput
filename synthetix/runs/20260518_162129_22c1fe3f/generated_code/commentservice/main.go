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

	r.Post("/api/commentservice/commentview", commentViewHandler)
	r.Post("/api/commentservice/createcommentrequest", createCommentRequestHandler)
	r.Post("/api/commentservice/tagrepository", tagRepositoryHandler)
	r.Post("/api/commentservice/globalcontrolleradvice", globalControllerAdviceHandler)

	port := os.Getenv("PORT")
	if port == "" {
		port = "8080"
	}

	log.Printf("Starting server on port %s", port)
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
