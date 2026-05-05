package main

import (
	"bytes"
	"encoding/json"
	"net/http"
	"net/http/httptest"
	"testing"
)

func TestRegisterHandler(t *testing.T) {
	body := SignupRequest{
		Username:       "testuser",
		Email:          "test@example.com",
		Password:       "password",
		RecaptchaToken: "token",
	}
	b, _ := json.Marshal(body)
	req, err := http.NewRequest("POST", "/api/auth/register", bytes.NewReader(b))
	if err != nil {
		t.Fatal(err)
	}

	rec := httptest.NewRecorder()
	handler := http.HandlerFunc(registerHandler)

	handler.ServeHTTP(rec, req)

	if status := rec.Code; status != http.StatusOK {
		t.Errorf("handler returned wrong status code: got %v want %v", status, http.StatusOK)
	}
}

func TestLoginHandler_InvalidCredentials(t *testing.T) {
	body := LoginRequest{
		Username:       "wronguser",
		Password:       "wrongpassword",
		RecaptchaToken: "token",
	}
	b, _ := json.Marshal(body)
	req, err := http.NewRequest("POST", "/api/auth/login", bytes.NewReader(b))
	if err != nil {
		t.Fatal(err)
	}

	rec := httptest.NewRecorder()
	handler := http.HandlerFunc(loginHandler)

	handler.ServeHTTP(rec, req)

	if status := rec.Code; status != http.StatusUnauthorized {
		t.Errorf("handler returned wrong status code: got %v want %v", status, http.StatusUnauthorized)
	}
}

func TestRepositoryRoundTrip(t *testing.T) {
	// Mock database connection and repository
	db := sqlx.MustConnect("postgres", "")
	repo := UserRepository{db: db}

	event := UserEvent{ID: "1", EventType: "login", Timestamp: "2023-10-01T12:00:00Z"}

	// Create event
	err := repo.CreateUserEvent(context.Background(), event)
	if err != nil {
		t.Fatalf("failed to create user event: %v", err)
	}

	// Retrieve event
	result, err := repo.FindUserEventByID(context.Background(), "1")
	if err != nil {
		t.Fatalf("failed to find user event: %v", err)
	}

	if result.ID != event.ID || result.EventType != event.EventType || result.Timestamp != event.Timestamp {
		t.Errorf("retrieved event does not match: got %+v want %+v", result, event)
	}
}
