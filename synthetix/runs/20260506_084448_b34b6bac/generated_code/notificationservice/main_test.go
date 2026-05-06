package main

import (
	"net/http"
	"net/http/httptest"
	"strings"
	"testing"
)

func TestHealthHandler(t *testing.T) {
	req := httptest.NewRequest("GET", "/health", nil)
	w := httptest.NewRecorder()
	healthHandler(w, req)

	resp := w.Result()
	if resp.StatusCode != http.StatusOK {
		t.Errorf("expected status 200, got %d", resp.StatusCode)
	}
}

func TestRegisterNotificationHandler(t *testing.T) {
	validPayload := `{"username":"testuser","email":"test@example.com","password":"password","recaptcha_token":"token"}`
	req := httptest.NewRequest("POST", "/api/notification/register", strings.NewReader(validPayload))
	w := httptest.NewRecorder()
	registerNotificationHandler(w, req)

	resp := w.Result()
	if resp.StatusCode != http.StatusOK {
		t.Errorf("expected status 200, got %d", resp.StatusCode)
	}

	invalidPayload := `{"username":"","email":"","password":"","recaptcha_token":""}`
	req = httptest.NewRequest("POST", "/api/notification/register", strings.NewReader(invalidPayload))
	w = httptest.NewRecorder()
	registerNotificationHandler(w, req)

	resp = w.Result()
	if resp.StatusCode != http.StatusBadRequest {
		t.Errorf("expected status 400, got %d", resp.StatusCode)
	}
}