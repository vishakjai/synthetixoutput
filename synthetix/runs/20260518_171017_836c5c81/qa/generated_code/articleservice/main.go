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

	r.Route("/api/articles", func(r chi.Router) {
		r.Post("/", createArticleHandler)
		r.Get("/", getArticlesHandler)
		r.Get("/feed", feedHandler)
		r.Get("/{slug}", getArticleHandler)
		r.Put("/{slug}", updateArticleHandler)
		r.Delete("/{slug}", deleteArticleHandler)
		r.Post("/{slug}/favorite", favoriteArticleHandler)
		r.Delete("/{slug}/favorite", unfavoriteArticleHandler)
		r.Get("/{slug}/comments", getCommentsHandler)
		r.Post("/{slug}/comments", addCommentHandler)
		r.Delete("/{slug}/comments/{commentId}", deleteCommentHandler)
	})

	r.Get("/api/tags", getTagsHandler)

	port := os.Getenv("PORT")
	if port == "" {
		port = "8080"
	}

	log.Printf("Starting server on port %s", port)
	if err := http.ListenAndServe(":"+port, r); err != nil {
		log.Fatalf("Failed to start server: %v", err)
	}
}

func healthHandler(w http.ResponseWriter, r *http.Request) {
	w.Write([]byte(`{"status": "healthy"}`))
}

func readyHandler(w http.ResponseWriter, r *http.Request) {
	w.Write([]byte(`{"status": "ready"}`))
}
