package main

import (
	"encoding/json"
	"net/http"
	"time"

	"github.com/golang-jwt/jwt/v5"
	"golang.org/x/crypto/bcrypt"
)

type SignupRequest struct {
	Username       string `json:"username"`
	Email          string `json:"email"`
	Password       string `json:"password"`
	RecaptchaToken string `json:"recaptcha_token"`
}

type SignupResponse struct {
	UserID    string `json:"user_id"`
	Username  string `json:"username"`
	CreatedAt string `json:"created_at"`
}

type LoginRequest struct {
	Username       string `json:"username"`
	Password       string `json:"password"`
	RecaptchaToken string `json:"recaptcha_token"`
}

type JwtAuthResponse struct {
	Token       string   `json:"token"`
	TokenType   string   `json:"token_type"`
	Username    string   `json:"username"`
	Authorities []string `json:"authorities"`
	ExpiresAt   string   `json:"expires_at"`
}

type RefreshTokenRequest struct {
	RefreshToken string `json:"refresh_token"`
}

type LogoutResponse struct {
	LoggedOut bool `json:"logged_out"`
}

type PasswordResetRequest struct {
	Email string `json:"email"`
}

type PasswordResetResponse struct {
	ResetInitiated bool `json:"reset_initiated"`
}

func apiKeyHandler(w http.ResponseWriter, r *http.Request) {
	writeJSON(w, http.StatusOK, map[string]string{"status": "ok"})
}

func registerHandler(w http.ResponseWriter, r *http.Request) {
	var req SignupRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}
	if req.Username == "" || req.Email == "" || req.Password == "" || req.RecaptchaToken == "" {
		writeError(w, http.StatusBadRequest, "VALIDATION_ERROR", "All fields are required")
		return
	}
	// Simulate registration logic
	writeJSON(w, http.StatusOK, SignupResponse{UserID: "123e4567-e89b-12d3-a456-426614174000", Username: req.Username, CreatedAt: time.Now().Format(time.RFC3339)})
}

func loginHandler(w http.ResponseWriter, r *http.Request) {
	var req LoginRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}
	if req.Username == "" || req.Password == "" || req.RecaptchaToken == "" {
		writeError(w, http.StatusBadRequest, "VALIDATION_ERROR", "All fields are required")
		return
	}
	// Simulate login logic
	writeJSON(w, http.StatusOK, JwtAuthResponse{Token: "dummy-token", TokenType: "Bearer", Username: req.Username, Authorities: []string{"user"}, ExpiresAt: time.Now().Add(24 * time.Hour).Format(time.RFC3339)})
}

func refreshHandler(w http.ResponseWriter, r *http.Request) {
	var req RefreshTokenRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}
	if req.RefreshToken == "" {
		writeError(w, http.StatusBadRequest, "VALIDATION_ERROR", "Refresh token is required")
		return
	}
	// Simulate refresh logic
	writeJSON(w, http.StatusOK, map[string]string{"token": "new-dummy-token", "expires_at": time.Now().Add(24 * time.Hour).Format(time.RFC3339)})
}

func logoutHandler(w http.ResponseWriter, r *http.Request) {
	writeJSON(w, http.StatusOK, LogoutResponse{LoggedOut: true})
}

func resendVerificationHandler(w http.ResponseWriter, r *http.Request) {
	writeJSON(w, http.StatusOK, map[string]string{"status": "verification resent"})
}

func verifyHandler(w http.ResponseWriter, r *http.Request) {
	writeJSON(w, http.StatusOK, map[string]string{"status": "verified"})
}

func forgotPasswordHandler(w http.ResponseWriter, r *http.Request) {
	var req PasswordResetRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}
	if req.Email == "" {
		writeError(w, http.StatusBadRequest, "VALIDATION_ERROR", "Email is required")
		return
	}
	// Simulate forgot password logic
	writeJSON(w, http.StatusOK, PasswordResetResponse{ResetInitiated: true})
}

func resetPasswordHandler(w http.ResponseWriter, r *http.Request) {
	var req PasswordResetRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}
	if req.Email == "" {
		writeError(w, http.StatusBadRequest, "VALIDATION_ERROR", "Email is required")
		return
	}
	// Simulate reset password logic
	writeJSON(w, http.StatusOK, PasswordResetResponse{ResetInitiated: true})
}

func changePasswordHandler(w http.ResponseWriter, r *http.Request) {
	writeJSON(w, http.StatusOK, map[string]string{"status": "password changed"})
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
