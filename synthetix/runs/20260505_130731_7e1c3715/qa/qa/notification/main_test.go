package main

import (
	"bytes"
	"net/http"
	"net/http/httptest"
	"testing"
)

func TestRegisterNotificationHandler(t *testing.T) {
	body := `{"username": "testuser", "email": "test@example.com", "password": "password", "recaptcha_token": "token"}`
	req, err := http.NewRequest("POST", "/api/notification/register", bytes.NewBuffer([]byte(body)))
	if err != nil {
		t.Fatal(err)
	}

	recorder := httptest.NewRecorder()
	handler := http.HandlerFunc(registerNotificationHandler)

	handler.ServeHTTP(recorder, req)

	if status := recorder.Code; status != http.StatusOK {
		t.Errorf("handler returned wrong status code: got %v want %v", status, http.StatusOK)
	}
}

func TestRegisterNotificationHandlerValidation(t *testing.T) {
	body := `{"username": "", "email": "", "password": "", "recaptcha_token": ""}`
	req, err := http.NewRequest("POST", "/api/notification/register", bytes.NewBuffer([]byte(body)))
	if err != nil {
		t.Fatal(err)
	}

	recorder := httptest.NewRecorder()
	handler := http.HandlerFunc(registerNotificationHandler)

	handler.ServeHTTP(recorder, req)

	if status := recorder.Code; status != http.StatusBadRequest {
		t.Errorf("handler returned wrong status code: got %v want %v", status, http.StatusBadRequest)
	}
}
