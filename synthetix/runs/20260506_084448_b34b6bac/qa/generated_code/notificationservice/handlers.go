package main

import (
	"encoding/json"
	"net/http"
)

type RegisterRequest struct {
	Username       string `json:"username"`
	Email          string `json:"email"`
	Password       string `json:"password"`
	RecaptchaToken string `json:"recaptcha_token"`
}

type RegisterResponse struct {
	UserID    string `json:"user_id"`
	Username  string `json:"username"`
	CreatedAt string `json:"created_at"`
}

func registerNotificationHandler(w http.ResponseWriter, r *http.Request) {
	var req RegisterRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}

	if req.Username == "" || req.Email == "" || req.Password == "" || req.RecaptchaToken == "" {
		writeError(w, http.StatusBadRequest, "VALIDATION_ERROR", "All fields are required")
		return
	}

	response := RegisterResponse{
		UserID:    "123e4567-e89b-12d3-a456-426614174000",
		Username:  req.Username,
		CreatedAt: "2023-10-01T12:00:00Z",
	}

	writeJSON(w, http.StatusOK, response)
}

func deleteNotificationHandler(w http.ResponseWriter, r *http.Request) {
	token := chi.URLParam(r, "token")
	if token == "" {
		writeError(w, http.StatusBadRequest, "VALIDATION_ERROR", "Token is required")
		return
	}

	writeJSON(w, http.StatusOK, map[string]string{
		"message": "Notification deleted successfully",
	})
}

func healthHandler(w http.ResponseWriter, r *http.Request) {
	writeJSON(w, http.StatusOK, map[string]string{"status": "healthy"})
}

func readyHandler(w http.ResponseWriter, r *http.Request) {
	writeJSON(w, http.StatusOK, map[string]string{"status": "ready"})
}

func writeError(w http.ResponseWriter, status int, code, message string) {
	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(status)
	json.NewEncoder(w).Encode(map[string]string{"code": code, "message": message})
}

func writeJSON(w http.ResponseWriter, status int, payload any) {
	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(status)
	json.NewEncoder(w).Encode(payload)
}