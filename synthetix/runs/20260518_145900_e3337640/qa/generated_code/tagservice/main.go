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

	r.Post("/api/tagservice/taglistview", tagListViewHandler)
	r.Post("/api/tagservice/tagrepository", tagRepositoryHandler)
	r.Post("/api/tagservice/profileview", profileViewHandler)
	r.Post("/api/tagservice/localeconfigurer", localeConfigurerHandler)
	r.Post("/api/tagservice/notblankornull", notBlankOrNullHandler)

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
	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status": "healthy"}`))
}

func readyHandler(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status": "ready"}`))
}
