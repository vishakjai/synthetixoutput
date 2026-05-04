package tests

import (
	"net/http"
	"net/http/httptest"
	"testing"

	"github.com/go-chi/chi/v5"
)

func TestChatEndpoint(t *testing.T) {
	r := chi.NewRouter()
	r.Get("/chatmodule/execute", func(w http.ResponseWriter, r *http.Request) {
		w.WriteHeader(http.StatusOK)
		w.Write([]byte(`{"message": "Chat operation executed"}`))
	})

	req, _ := http.NewRequest("GET", "/chatmodule/execute", nil)
	resp := httptest.NewRecorder()
	r.ServeHTTP(resp, req)

	if status := resp.Code; status != http.StatusOK {
		t.Errorf("handler returned wrong status code: got %v want %v", status, http.StatusOK)
	}

	expected := `{"message": "Chat operation executed"}`
	if resp.Body.String() != expected {
		t.Errorf("handler returned unexpected body: got %v want %v", resp.Body.String(), expected)
	}
}
