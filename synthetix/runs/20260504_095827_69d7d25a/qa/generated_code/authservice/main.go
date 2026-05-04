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

	r.Post("/api/auth/apiKey", apiKeyHandler)
	r.Post("/api/auth/register", registerHandler)
	r.Post("/api/auth/login", loginHandler)
	r.Post("/api/auth/refresh", refreshHandler)
	r.Post("/api/auth/logout", logoutHandler)
	r.Post("/api/auth/resend-verification", resendVerificationHandler)
	r.Post("/api/auth/verify", verifyHandler)
	r.Post("/api/auth/forgot-password", forgotPasswordHandler)

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
