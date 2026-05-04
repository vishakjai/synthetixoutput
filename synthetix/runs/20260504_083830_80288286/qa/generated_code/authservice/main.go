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

	r.Get("/health", func(w http.ResponseWriter, r *http.Request) {
		w.Write([]byte(`{"status": "healthy"}`))
	})

	r.Get("/ready", func(w http.ResponseWriter, r *http.Request) {
		w.Write([]byte(`{"status": "ready"}`))
	})

	r.Mount("/api/auth", authRouter())

	port := os.Getenv("PORT")
	if port == "" {
		port = "8080"
	}

	log.Printf("Starting server on :%s", port)
	if err := http.ListenAndServe(":"+port, r); err != nil {
		log.Fatalf("Error starting server: %v", err)
	}
}

func authRouter() http.Handler {
	r := chi.NewRouter()

	r.Post("/apiKey", apiKeyHandler)
	r.Post("/register", registerHandler)
	r.Post("/login", loginHandler)
	r.Post("/refresh", refreshHandler)
	r.Post("/logout", logoutHandler)
	r.Post("/resend-verification", resendVerificationHandler)
	r.Post("/verify", verifyHandler)
	r.Post("/forgot-password", forgotPasswordHandler)

	return r
}