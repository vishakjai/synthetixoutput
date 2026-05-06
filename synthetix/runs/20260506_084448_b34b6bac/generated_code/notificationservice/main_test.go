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
	body := strings.NewReader(`{"username":"testuser","email":"test@example.com","password":"password","recaptcha_token":"token"}`)
	req := httptest.NewRequest("POST", "/api/notification/register", body)
	w := httptest.NewRecorder()

	registerNotificationHandler(w, req)

	resp := w.Result()
	if resp.StatusCode != http.StatusOK {
		t.Errorf("expected status 200, got %d", resp.StatusCode)
	}
}

func TestRegisterNotificationHandler_MissingFields(t *testing.T) {
	body := strings.NewReader(`{"username":"","email":"","password":"","recaptcha_token":""}`)
	req := httptest.NewRequest("POST", "/api/notification/register", body)
	w := httptest.NewRecorder()

	registerNotificationHandler(w, req)

	resp := w.Result()
	if resp.StatusCode != http.StatusBadRequest {
		t.Errorf("expected status 400, got %d", resp.StatusCode)
	}
}
