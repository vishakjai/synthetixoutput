package main

import (
	"encoding/json"
	"net/http"
	"time"

	"github.com/golang-jwt/jwt/v5"
	"golang.org/x/crypto/bcrypt"
)

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
		writeError(w, http.StatusBadRequest, "VALIDATION_ERROR", "all fields are required")
		return
	}
	// Simulate user registration logic
	writeJSON(w, http.StatusOK, SignupResponse{
		UserID:    "123e4567-e89b-12d3-a456-426614174000",
		Username:  req.Username,
		CreatedAt: time.Now().Format(time.RFC3339),
	})
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
	token, err := signJWT("123e4567-e89b-12d3-a456-426614174000", []string{"user"}, []byte("secret"), 24*time.Hour)
	if err != nil {
		writeError(w, http.StatusInternalServerError, "INTERNAL", "token issue failed")
		return
	}
	writeJSON(w, http.StatusOK, JwtAuthResponse{
		Token:       token,
		TokenType:   "Bearer",
		Username:    req.Username,
		Authorities: []string{"user"},
		ExpiresAt:   time.Now().Add(24 * time.Hour).Format(time.RFC3339),
	})
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
	token, err := signJWT("123e4567-e89b-12d3-a456-426614174000", []string{"user"}, []byte("secret"), 24*time.Hour)
	if err != nil {
		writeError(w, http.StatusInternalServerError, "INTERNAL", "token issue failed")
		return
	}
	writeJSON(w, http.StatusOK, map[string]string{
		"token":     token,
		"expires_at": time.Now().Add(24 * time.Hour).Format(time.RFC3339),
	})
}

func logoutHandler(w http.ResponseWriter, r *http.Request) {
	writeJSON(w, http.StatusOK, LogoutResponse{LoggedOut: true})
}

func resendVerificationHandler(w http.ResponseWriter, r *http.Request) {
	writeJSON(w, http.StatusOK, map[string]string{"status": "verification email resent"})
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
		writeError(w, http.StatusBadRequest, "VALIDATION_ERROR", "email is required")
		return
	}
	// Simulate password reset initiation logic
	writeJSON(w, http.StatusOK, PasswordResetResponse{ResetInitiated: true})
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

func signJWT(userID string, roles []string, secret []byte, ttl time.Duration) (string, error) {
	claims := jwt.MapClaims{
		"sub":  userID,
		"roles": roles,
		"exp":  time.Now().Add(ttl).Unix(),
		"iat":  time.Now().Unix(),
	}
	return jwt.NewWithClaims(jwt.SigningMethodHS256, claims).SignedString(secret)
}