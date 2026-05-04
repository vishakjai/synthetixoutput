package main

import (
	"net/http"
	"net/http/httptest"
	"testing"

	"github.com/go-chi/chi/v5"
	"notificationmodule/src/Application/controllers"
)

func TestExecuteNotification(t *testing.T) {
	r := chi.NewRouter()
	r.Post("/notificationmodule/execute", controllers.ExecuteNotification)

	ts := httptest.NewServer(r)
	defer ts.Close()

	resp, err := http.Post(ts.URL+"/notificationmodule/execute", "application/json", strings.NewReader(`{"message": "test"}`))
	if err != nil {
		t.Fatalf("Failed to send request: %v", err)
	}

	if resp.StatusCode != http.StatusOK {
		t.Errorf("Expected status 200, got %d", resp.StatusCode)
	}
}