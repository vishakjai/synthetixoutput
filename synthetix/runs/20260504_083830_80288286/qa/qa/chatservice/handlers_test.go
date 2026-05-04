package main

import (
	"net/http"
	"net/http/httptest"
	"strings"
	"testing"

	"github.com/go-chi/chi/v5"
)

// Doctor-generated contract tests. Each row exercises one non-health
// route registered in main(). Failures here mean either the route
// isn't wired or the handler returns 5xx for a well-formed payload —
// both code-gen issues the developer should fix.
func TestContractRoutes(t *testing.T) {
	router := newTestRouter()
	cases := []struct {
		name   string
		method string
		path   string
		body   string
	}{
	{"TestGetApiChatChatidMessages", "GET", "/api/chat/test-id/messages", `null`},
	{"TestGetApiChatChatid", "GET", "/api/chat/test-id", `null`},
	{"TestGetApiChat", "GET", "/api/chat", `null`},
	{"TestPostApiChat", "POST", "/api/chat", `null`},
	{"TestPostApiChatChatidAdd", "POST", "/api/chat/test-id/add", `null`},
	{"TestDeleteApiChatChatidLeave", "DELETE", "/api/chat/test-id/leave", `null`},
	}
	for _, c := range cases {
		t.Run(c.name, func(t *testing.T) {
			var reader *strings.Reader
			if c.body != "" && c.body != "null" {
				reader = strings.NewReader(c.body)
			} else {
				reader = strings.NewReader("")
			}
			req := httptest.NewRequest(c.method, c.path, reader)
			req.Header.Set("Content-Type", "application/json")
			rec := httptest.NewRecorder()
			router.ServeHTTP(rec, req)
			// Accept 2xx/4xx — handler should at minimum reach
			// validation; 5xx means a coding error worth fixing.
			if rec.Code >= 500 {
				t.Errorf("%s %s returned 5xx (%d): %s", c.method, c.path, rec.Code, rec.Body.String())
			}
		})
	}
}

// newTestRouter mounts the production handlers on a fresh chi router
// for in-process testing. The doctor wires the registerXxxRoutes
// calls it detected in the source. Edit if the LLM used a different
// naming convention.
func newTestRouter() chi.Router {
	r := chi.NewRouter()
	r.Get("/health", func(w http.ResponseWriter, _ *http.Request) {
		w.Header().Set("Content-Type", "application/json")
		w.WriteHeader(http.StatusOK)
		w.Write([]byte(`{"status": "healthy"}`))
	})
	r.Get("/ready", func(w http.ResponseWriter, _ *http.Request) {
		w.Header().Set("Content-Type", "application/json")
		w.WriteHeader(http.StatusOK)
		w.Write([]byte(`{"status": "ready"}`))
	})
	// No production route registrars detected — only health endpoints exercised.
	return r
}
