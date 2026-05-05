package main

import (
	"bytes"
	"net/http"
	"net/http/httptest"
	"testing"
)

func TestRegisterHandler_ValidationFailure(t *testing.T) {
	reqBody := `{"username": "", "email": "", "password": "", "recaptcha_token": ""}`
	req := httptest.NewRequest(http.MethodPost, "/api/auth/register", bytes.NewBufferString(reqBody))
	w := httptest.NewRecorder()

	handler := http.HandlerFunc(registerHandler)
	handler.ServeHTTP(w, req)

	if w.Code != http.StatusBadRequest {
		t.Errorf("expected status 400, got %d", w.Code)
	}
}

func TestLoginHandler_InvalidCredentials(t *testing.T) {
	reqBody := `{"username": "wrong", "password": "wrong", "recaptcha_token": "valid"}`
	req := httptest.NewRequest(http.MethodPost, "/api/auth/login", bytes.NewBufferString(reqBody))
	w := httptest.NewRecorder()

	handler := http.HandlerFunc(loginHandler)
	handler.ServeHTTP(w, req)

	if w.Code != http.StatusUnauthorized {
		t.Errorf("expected status 401, got %d", w.Code)
	}
}

func TestUserEventRepository_CreateAndGet(t *testing.T) {
	// Mock database setup and teardown
	// This is a placeholder for actual database interaction tests
}