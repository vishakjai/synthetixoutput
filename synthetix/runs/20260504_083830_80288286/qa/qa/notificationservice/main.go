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
	r.Post("/api/notification/register", registerDeviceTokenHandler)
	r.Delete("/api/notification/{token}", deleteNotificationTokenHandler)
	r.Get("/api/participants", getParticipantsHandler)
	r.Post("/api/participants/profile-picture-upload", uploadProfilePictureHandler)
	r.Post("/api/participants/confirm-profile-picture", confirmProfilePictureHandler)
	r.Delete("/api/participants/profile-picture", deleteProfilePictureHandler)

	port := os.Getenv("PORT")
	if port == "" {
		port = "8080"
	}

	log.Printf("Starting server on port %s", port)
	if err := http.ListenAndServe(":"+port, r); err != nil {
		log.Fatalf("Could not start server: %s", err)
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
