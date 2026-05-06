package main

import (
	"bytes"
	"encoding/json"
	"net/http"
	"net/http/httptest"
	"testing"
)

func TestLoginHandler_Success(t *testing.T) {
	body := LoginRequest{Username: "user", Password: "pass", RecaptchaToken: "token"}
	jsonBody, _ := json.Marshal(body)
	req, err := http.NewRequest("POST", "/api/auth/login", bytes.NewBuffer(jsonBody))
	if err != nil {
		t.Fatalf("Could not create request: %v", err)
	}

	recorder := httptest.NewRecorder()

	handler := http.HandlerFunc(loginHandler)
	handler.ServeHTTP(recorder, req)

	if status := recorder.Code; status != http.StatusOK {
		t.Errorf("handler returned wrong status code: got %v want %v", status, http.StatusOK)
	}

	var response JwtAuthResponse
	if err := json.NewDecoder(recorder.Body).Decode(&response); err != nil {
		t.Errorf("Could not decode response: %v", err)
	}

	if response.Username != "user" {
		t.Errorf("Expected username 'user', got %v", response.Username)
	}
}

func TestLoginHandler_InvalidCredentials(t *testing.T) {
	body := LoginRequest{Username: "user", Password: "wrongpass", RecaptchaToken: "token"}
	jsonBody, _ := json.Marshal(body)
	req, err := http.NewRequest("POST", "/api/auth/login", bytes.NewBuffer(jsonBody))
	if err != nil {
		t.Fatalf("Could not create request: %v", err)
	}

	recorder := httptest.NewRecorder()

	handler := http.HandlerFunc(loginHandler)
	handler.ServeHTTP(recorder, req)

	if status := recorder.Code; status != http.StatusUnauthorized {
		t.Errorf("handler returned wrong status code: got %v want %v", status, http.StatusUnauthorized)
	}
}
