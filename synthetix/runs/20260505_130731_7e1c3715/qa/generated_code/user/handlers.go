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
	Token      string   `json:"token"`
	TokenType  string   `json:"token_type"`
	Username   string   `json:"username"`
	Authorities []string `json:"authorities"`
	ExpiresAt  string   `json:"expires_at"`
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
		writeError(w, http.StatusBadRequest, "VALIDATION_ERROR", "All fields are required")
		return
	}
	userID := "123e4567-e89b-12d3-a456-426614174000" // Simulated user ID
	resp := SignupResponse{
		UserID:    userID,
		Username:  req.Username,
		CreatedAt: time.Now().Format(time.RFC3339),
	}
	writeJSON(w, http.StatusOK, resp)
}

func loginHandler(w http.ResponseWriter, r *http.Request) {
	var req LoginRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}
	if req.Username == "" || req.Password == "" {
		writeError(w, http.StatusBadRequest, "VALIDATION_ERROR", "username and password required")
		return
	}
	// Simulate user authentication
	if req.Username != "testuser" || req.Password != "password" {
		writeError(w, http.StatusUnauthorized, "INVALID_CREDENTIALS", "invalid username or password")
		return
	}
	token, err := signJWT("123", []string{"user"}, []byte("secret"), 24*time.Hour)
	if err != nil {
		writeError(w, http.StatusInternalServerError, "INTERNAL", "token issue failed")
		return
	}
	resp := JwtAuthResponse{
		Token:      token,
		TokenType:  "Bearer",
		Username:   req.Username,
		Authorities: []string{"user"},
		ExpiresAt:  time.Now().Add(24 * time.Hour).Format(time.RFC3339),
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
		writeError(w, http.StatusBadRequest, "VALIDATION_ERROR", "refresh token required")
		return
	}
	// Simulate token refresh
	token, err := signJWT("123", []string{"user"}, []byte("secret"), 24*time.Hour)
	if err != nil {
		writeError(w, http.StatusInternalServerError, "INTERNAL", "token issue failed")
		return
	}
	resp := JwtAuthResponse{
		Token:      token,
		TokenType:  "Bearer",
		Username:   "testuser",
		Authorities: []string{"user"},
		ExpiresAt:  time.Now().Add(24 * time.Hour).Format(time.RFC3339),
	}
	writeJSON(w, http.StatusOK, resp)
}

func logoutHandler(w http.ResponseWriter, r *http.Request) {
	resp := LogoutResponse{LoggedOut: true}
	writeJSON(w, http.StatusOK, resp)
}

func resendVerificationHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status": "verification email sent"}`))
}

func verifyHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status": "verified"}`))
}

func forgotPasswordHandler(w http.ResponseWriter, r *http.Request) {
	var req PasswordResetRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}
	if req.Email == "" {
		writeError(w, http.StatusBadRequest, "VALIDATION_ERROR", "email required")
		return
	}
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
		writeError(w, http.StatusBadRequest, "VALIDATION_ERROR", "email required")
		return
	}
	resp := PasswordResetResponse{ResetInitiated: true}
	writeJSON(w, http.StatusOK, resp)
}

func changePasswordHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status": "password changed"}`))
}

func signJWT(userID string, roles []string, secret []byte, ttl time.Duration) (string, error) {
	claims := jwt.MapClaims{
		"sub":  userID,
		"roles": roles,
		"exp":  time.Now().Add(ttl).Unix(),
		"iat":  time.Now().Unix(),
	}
	return jwt.NewWithClaims(jwt.SigningMethodHS256, claims).SignedString(secret)
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
