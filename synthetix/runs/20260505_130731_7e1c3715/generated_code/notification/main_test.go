package main

import (
	"net/http"
	"net/http/httptest"
	"strings"
	"testing"
)

func TestRegisterHandler(t *testing.T) {
	body := `{"username":"testuser","email":"test@example.com","password":"password","recaptcha_token":"token"}`
	req, err := http.NewRequest("POST", "/api/notification/register", strings.NewReader(body))
	if err != nil {
		t.Fatal(err)
	}

	rec := httptest.NewRecorder()
	http.HandlerFunc(registerHandler).ServeHTTP(rec, req)

	if status := rec.Code; status != http.StatusOK {
		t.Errorf("handler returned wrong status code: got %v want %v", status, http.StatusOK)
	}
}

func TestRegisterHandlerValidationError(t *testing.T) {
	body := `{"username":"","email":"","password":"","recaptcha_token":""}`
	req, err := http.NewRequest("POST", "/api/notification/register", strings.NewReader(body))
	if err != nil {
		t.Fatal(err)
	}

	rec := httptest.NewRecorder()
	http.HandlerFunc(registerHandler).ServeHTTP(rec, req)

	if status := rec.Code; status != http.StatusBadRequest {
		t.Errorf("handler returned wrong status code: got %v want %v", status, http.StatusBadRequest)
	}
}
