package main

import (
"log"
	"net/http"
	"os"

	"github.com/go-chi/chi/v5"
	_ "github.com/lib/pq"
)

func main() {
	r := chi.NewRouter()

	r.Get("/health", healthHandler)
	r.Get("/ready", readyHandler)

	r.Post("/api/articles", createArticleHandler)
	r.Get("/api/articles", getArticlesHandler)
	r.Get("/api/articles/feed", feedHandler)
	r.Get("/api/articles/{slug}", getArticleHandler)
	r.Put("/api/articles/{slug}", updateArticleHandler)
	r.Delete("/api/articles/{slug}", deleteArticleHandler)
	r.Get("/api/articles/{slug}/comments", getCommentsHandler)
	r.Post("/api/articles/{slug}/comments", addCommentHandler)
	r.Delete("/api/articles/{slug}/comments/{commentId}", deleteCommentHandler)
	r.Get("/api/tags", getTagsHandler)

	port := os.Getenv("PORT")
	if port == "" {
		port = "8080"
	}

	log.Printf("Starting server on port %s", port)
	if err := http.ListenAndServe(":"+port, r); err != nil {
		log.Fatalf("could not start server: %v", err)
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