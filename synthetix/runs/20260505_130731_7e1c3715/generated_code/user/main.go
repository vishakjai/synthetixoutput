package main

import (
	"log"
	"net/http"
	"os"

	"github.com/go-chi/chi/v5"
	"github.com/jmoiron/sqlx"
	_ "github.com/lib/pq"
)

func main() {
	port := ":8080"
	if p := os.Getenv("PORT"); p != "" {
		port = ":" + p
	}

	db, err := sqlx.Open("postgres", os.Getenv("DATABASE_URL"))
	if err != nil {
		log.Fatalf("Failed to connect to database: %v", err)
	}
	defer db.Close()

	r := chi.NewRouter()

	r.Get("/health", func(w http.ResponseWriter, r *http.Request) {
		w.Write([]byte(`{"status":"healthy"}`))
	})

	r.Get("/ready", func(w http.ResponseWriter, r *http.Request) {
		w.Write([]byte(`{"status":"ready"}`))
	})

	// Register handlers
	r.Post("/api/auth/apiKey", apiKeyHandler)
	r.Post("/api/auth/register", registerHandler)
	r.Post("/api/auth/login", loginHandler)
	r.Post("/api/auth/refresh", refreshHandler)
	r.Post("/api/auth/logout", logoutHandler)
	r.Post("/api/auth/resend-verification", resendVerificationHandler)
	r.Post("/api/auth/verify", verifyHandler)
	r.Post("/api/auth/forgot-password", forgotPasswordHandler)
	r.Post("/api/auth/reset-password", resetPasswordHandler)
	r.Post("/api/auth/change-password", changePasswordHandler)

	log.Printf("Starting server on %s", port)
	if err := http.ListenAndServe(port, r); err != nil {
		log.Fatalf("Server failed: %v", err)
	}
}