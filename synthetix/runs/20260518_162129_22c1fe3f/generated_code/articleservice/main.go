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

	// Article endpoints
	r.Post("/api/articles", createArticleHandler)
	r.Get("/api/articles", getArticlesHandler)
	r.Get("/api/articles/feed", feedHandler)
	r.Get("/api/articles/{slug}", getArticleHandler)
	r.Put("/api/articles/{slug}", updateArticleHandler)
	r.Delete("/api/articles/{slug}", deleteArticleHandler)
	r.Post("/api/articles/{slug}/favorite", favoriteArticleHandler)
	r.Delete("/api/articles/{slug}/favorite", unfavoriteArticleHandler)
	r.Get("/api/articles/{slug}/comments", getCommentsHandler)
	r.Post("/api/articles/{slug}/comments", addCommentHandler)
	r.Delete("/api/articles/{slug}/comments/{commentId}", deleteCommentHandler)
	r.Get("/api/tags", getTagsHandler)

	port := os.Getenv("PORT")
	if port == "" {
		port = "8080"
	}

	log.Fatal(http.ListenAndServe(":"+port, r))
}

func healthHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status": "healthy"}`))
}

func readyHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status": "ready"}`))
}
