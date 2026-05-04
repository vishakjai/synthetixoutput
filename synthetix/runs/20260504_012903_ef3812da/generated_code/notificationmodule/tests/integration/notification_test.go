package integration

import (
	"net/http"
	"net/http/httptest"
	"testing"

	"github.com/go-chi/chi/v5"
	"notificationmodule/src/Application/controllers"
)

func TestExecuteNotification(t *testing.T) {
	r := chi.NewRouter()
	r.Get("/notificationmodule/execute", controllers.ExecuteNotification)

	req, _ := http.NewRequest("GET", "/notificationmodule/execute", nil)
	resp := httptest.NewRecorder()
	r.ServeHTTP(resp, req)

	if resp.Code != http.StatusOK {
		t.Fatalf("expected status 200 but got %d", resp.Code)
	}

	expected := `{"message": "Notification executed successfully"}`
	if resp.Body.String() != expected {
		t.Fatalf("expected body %s but got %s", expected, resp.Body.String())
	}
}