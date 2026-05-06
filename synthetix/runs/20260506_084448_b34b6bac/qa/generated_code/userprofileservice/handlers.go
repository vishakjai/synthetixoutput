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
	UserID    string    `json:"user_id"`
	Username  string    `json:"username"`
	CreatedAt time.Time `json:"created_at"`
}

type LoginRequest struct {
	Username       string `json:"username"`
	Password       string `json:"password"`
	RecaptchaToken string `json:"recaptcha_token"`
}

type JwtAuthResponse struct {
	Token      string   `json:"token"`
	TokenType  string   `json:"token_type"`
	Username   string   `json:"username"`
	Authorities []string `json:"authorities"`
	ExpiresAt  time.Time `json:"expires_at"`
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
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status": "ok"}`))
}

func registerHandler(w http.ResponseWriter, r *http.Request) {
	var req SignupRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}
	if req.Username == "" || req.Email == "" || req.Password == "" || req.RecaptchaToken == "" {
		writeError(w, http.StatusBadRequest, "VALIDATION_ERROR", "all fields are required")
		return
	}
	// Simulate user creation logic
	resp := SignupResponse{
		UserID:    "123e4567-e89b-12d3-a456-426614174000",
		Username:  req.Username,
		CreatedAt: time.Now(),
	}
	writeJSON(w, http.StatusOK, resp)
}

func loginHandler(w http.ResponseWriter, r *http.Request) {
	var req LoginRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}
	if req.Username == "" || req.Password == "" || req.RecaptchaToken == "" {
		writeError(w, http.StatusBadRequest, "VALIDATION_ERROR", "all fields are required")
		return
	}
	// Simulate login logic
	token := "dummy-token"
	resp := JwtAuthResponse{
		Token:      token,
		TokenType:  "Bearer",
		Username:   req.Username,
		Authorities: []string{"user"},
		ExpiresAt:  time.Now().Add(24 * time.Hour),
	}
	writeJSON(w, http.StatusOK, resp)
}

func refreshHandler(w http.ResponseWriter, r *http.Request) {
	var req RefreshTokenRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}
	if req.RefreshToken == "" {
		writeError(w, http.StatusBadRequest, "VALIDATION_ERROR", "refresh token is required")
		return
	}
	// Simulate token refresh logic
	token := "new-dummy-token"
	writeJSON(w, http.StatusOK, map[string]interface{}{
		"token":     token,
		"expires_at": time.Now().Add(24 * time.Hour),
	})
}

func logoutHandler(w http.ResponseWriter, r *http.Request) {
	// Simulate logout logic
	resp := LogoutResponse{LoggedOut: true}
	writeJSON(w, http.StatusOK, resp)
}

func resendVerificationHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status": "ok"}`))
}

func verifyHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status": "ok"}`))
}

func forgotPasswordHandler(w http.ResponseWriter, r *http.Request) {
	var req PasswordResetRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}
	if req.Email == "" {
		writeError(w, http.StatusBadRequest, "VALIDATION_ERROR", "email is required")
		return
	}
	// Simulate password reset initiation logic
	resp := PasswordResetResponse{ResetInitiated: true}
	writeJSON(w, http.StatusOK, resp)
}

func resetPasswordHandler(w http.ResponseWriter, r *http.Request) {
	var req PasswordResetRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}
	if req.Email == "" {
		writeError(w, http.StatusBadRequest, "VALIDATION_ERROR", "email is required")
		return
	}
	// Simulate password reset logic
	resp := PasswordResetResponse{ResetInitiated: true}
	writeJSON(w, http.StatusOK, resp)
}

func changePasswordHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status": "ok"}`))
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
