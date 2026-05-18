package main

import (
	"encoding/json"
	"net/http"

	"github.com/go-chi/chi/v5"
)

func RegisterRoutes(router *chi.Mux, userService *UserService) {
	router.Get("/health", healthHandler)
	router.Get("/ready", readyHandler)

	router.Post("/api/users", userService.Signup)
	router.Post("/api/users/login", userService.Login)
	router.Get("/api/user", userService.GetCurrentUser)
	router.Put("/api/user", userService.UpdateUser)
	router.Get("/api/profiles/{username}", userService.GetProfile)
	router.Post("/api/profiles/{username}/follow", userService.Follow)
	router.Delete("/api/profiles/{username}/follow", userService.Unfollow)
}

func healthHandler(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(map[string]string{"status": "healthy"})
}

func readyHandler(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(map[string]string{"status": "ready"})
}
