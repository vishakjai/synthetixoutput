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

	// Article routes
	r.Route("/api/articles", func(r chi.Router) {
		r.Post("/", createArticleHandler)
		r.Get("/", getArticlesHandler)
		r.Get("/feed", feedHandler)
		r.Route("/{slug}", func(r chi.Router) {
			r.Get("/", getArticleHandler)
			r.Put("/", updateArticleHandler)
			r.Delete("/", deleteArticleHandler)
			r.Get("/comments", getCommentsHandler)
			r.Post("/comments", addCommentHandler)
			r.Delete("/comments/{commentId}", deleteCommentHandler)
			r.Post("/favorite", favoriteArticleHandler)
			r.Delete("/favorite", unfavoriteArticleHandler)
		})
	})

	r.Get("/api/tags", getTagsHandler)

	port := os.Getenv("PORT")
	if port == "" {
		port = "8080"
	}

	log.Printf("Starting server on port %s", port)
	if err := http.ListenAndServe(":"+port, r); err != nil {
		log.Fatalf("Error starting server: %v", err)
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
