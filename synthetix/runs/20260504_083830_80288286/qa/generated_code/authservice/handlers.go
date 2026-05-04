package main

import (
	"encoding/json"
	"net/http"
)

func apiKeyHandler(w http.ResponseWriter, r *http.Request) {
	writeJSON(w, http.StatusOK, map[string]string{"status": "success"})
}

func registerHandler(w http.ResponseWriter, r *http.Request) {
	var req SignupRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}
	// Simulate user registration logic
	writeJSON(w, http.StatusOK, SignupResponse{
		UserID:    "123e4567-e89b-12d3-a456-426614174000",
		Username:  req.Username,
		CreatedAt: "2023-10-01T12:00:00Z",
	})
}

func loginHandler(w http.ResponseWriter, r *http.Request) {
	var req LoginRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}
	// Simulate login logic
	writeJSON(w, http.StatusOK, JwtAuthResponse{
		Token:      "dummy-token",
		TokenType:  "Bearer",
		Username:   req.Username,
		Authorities: []string{"user"},
		ExpiresAt:  "2023-10-01T12:00:00Z",
	})
}

func refreshHandler(w http.ResponseWriter, r *http.Request) {
	var req RefreshTokenRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}
	// Simulate token refresh logic
	writeJSON(w, http.StatusOK, map[string]string{
		"token":     "new-dummy-token",
		"expires_at": "2023-10-01T12:00:00Z",
	})
}

func logoutHandler(w http.ResponseWriter, r *http.Request) {
	writeJSON(w, http.StatusOK, LogoutResponse{LoggedOut: true})
}

func resendVerificationHandler(w http.ResponseWriter, r *http.Request) {
	writeJSON(w, http.StatusOK, map[string]string{"status": "success"})
}

func verifyHandler(w http.ResponseWriter, r *http.Request) {
	writeJSON(w, http.StatusOK, map[string]string{"status": "success"})
}

func forgotPasswordHandler(w http.ResponseWriter, r *http.Request) {
	var req PasswordResetRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}
	// Simulate password reset logic
	writeJSON(w, http.StatusOK, PasswordResetResponse{ResetInitiated: true})
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
