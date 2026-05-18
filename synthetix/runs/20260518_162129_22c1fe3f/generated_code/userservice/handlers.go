package main

import (
	"encoding/json"
	"net/http"

	"github.com/go-chi/chi/v5"
)

func userRouter() http.Handler {
	r := chi.NewRouter()

	r.Post("/users", signupHandler)
	r.Post("/users/login", loginHandler)
	r.Get("/user", getCurrentUserHandler)
	r.Put("/user", updateUserHandler)
	r.Get("/profiles/{username}", getProfileHandler)
	r.Post("/profiles/{username}/follow", followHandler)
	r.Delete("/profiles/{username}/follow", unfollowHandler)

	return r
}

func signupHandler(w http.ResponseWriter, r *http.Request) {
	var req SignupRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}
	// Validate and process signup
	// Respond with SignupResponse
}

func loginHandler(w http.ResponseWriter, r *http.Request) {
	var req LoginRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}
	// Validate and process login
	// Respond with JwtAuthResponse
}

func getCurrentUserHandler(w http.ResponseWriter, r *http.Request) {
	// Implement get current user logic
}

func updateUserHandler(w http.ResponseWriter, r *http.Request) {
	// Implement update user logic
}

func getProfileHandler(w http.ResponseWriter, r *http.Request) {
	// Implement get profile logic
}

func followHandler(w http.ResponseWriter, r *http.Request) {
	// Implement follow logic
}

func unfollowHandler(w http.ResponseWriter, r *http.Request) {
	// Implement unfollow logic
}

func writeError(w http.ResponseWriter, status int, code, message string) {
	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(status)
	json.NewEncoder(w).Encode(map[string]string{"code": code, "message": message})
}
