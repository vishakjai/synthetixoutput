package main

import (
	"encoding/json"
	"net/http"
)

func apiKeyHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status": "success"}`))
}

func registerHandler(w http.ResponseWriter, r *http.Request) {
	var req SignupRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}
	// Validate and process registration
	// Simulate response
	resp := SignupResponse{
		UserID:    "123e4567-e89b-12d3-a456-426614174000",
		Username:  req.Username,
		CreatedAt: "2023-10-01T12:00:00Z",
	}
	writeJSON(w, http.StatusOK, resp)
}

func loginHandler(w http.ResponseWriter, r *http.Request) {
	var req LoginRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}
	// Simulate login logic
	resp := JwtAuthResponse{
		Token:      "dummy-token",
		TokenType:  "Bearer",
		Username:   req.Username,
		Authorities: []string{"user"},
		ExpiresAt:  "2023-10-02T12:00:00Z",
	}
	writeJSON(w, http.StatusOK, resp)
}

func refreshHandler(w http.ResponseWriter, r *http.Request) {
	var req RefreshTokenRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}
	// Simulate token refresh logic
	resp := JwtAuthResponse{
		Token:     "new-dummy-token",
		ExpiresAt: "2023-10-03T12:00:00Z",
	}
	writeJSON(w, http.StatusOK, resp)
}

func logoutHandler(w http.ResponseWriter, r *http.Request) {
	// Simulate logout logic
	resp := LogoutResponse{
		LoggedOut: true,
	}
	writeJSON(w, http.StatusOK, resp)
}

func resendVerificationHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status": "success"}`))
}

func verifyHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status": "success"}`))
}

func forgotPasswordHandler(w http.ResponseWriter, r *http.Request) {
	var req PasswordResetRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}
	// Simulate password reset logic
	resp := PasswordResetResponse{
		ResetInitiated: true,
	}
	writeJSON(w, http.StatusOK, resp)
}

func writeError(w http.ResponseWriter, status int, code, message string) {
	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(status)
	json.NewEncoder(w).Encode(map[string]string{"code": code, "message": message})
}

func writeJSON(w http.ResponseWriter, status int, payload interface{}) {
	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(status)
	json.NewEncoder(w).Encode(payload)
}
