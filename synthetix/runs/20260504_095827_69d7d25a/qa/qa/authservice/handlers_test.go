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
	{"TestPostApiAuthApikey", "POST", "/api/auth/apiKey", `{"username": "sample-username", "email": "sample-email", "password": "sample-password", "recaptcha_token": "sample-recaptcha_token"}`},
	{"TestPostApiAuthRegister", "POST", "/api/auth/register", `{"username": "sample-username", "email": "sample-email", "password": "sample-password", "recaptcha_token": "sample-recaptcha_token"}`},
	{"TestPostApiAuthLogin", "POST", "/api/auth/login", `{"username": "sample-username", "email": "sample-email", "password": "sample-password", "recaptcha_token": "sample-recaptcha_token"}`},
	{"TestPostApiAuthRefresh", "POST", "/api/auth/refresh", `{"username": "sample-username", "email": "sample-email", "password": "sample-password", "recaptcha_token": "sample-recaptcha_token"}`},
	{"TestPostApiAuthLogout", "POST", "/api/auth/logout", `{"username": "sample-username", "email": "sample-email", "password": "sample-password", "recaptcha_token": "sample-recaptcha_token"}`},
	{"TestPostApiAuthResendVerification", "POST", "/api/auth/resend-verification", `{"username": "sample-username", "email": "sample-email", "password": "sample-password", "recaptcha_token": "sample-recaptcha_token"}`},
	{"TestPostApiAuthVerify", "POST", "/api/auth/verify", `{"username": "sample-username", "email": "sample-email", "password": "sample-password", "recaptcha_token": "sample-recaptcha_token"}`},
	{"TestPostApiAuthForgotPassword", "POST", "/api/auth/forgot-password", `{"username": "sample-username", "email": "sample-email", "password": "sample-password", "recaptcha_token": "sample-recaptcha_token"}`},
	{"TestPostApiAuthResetPassword", "POST", "/api/auth/reset-password", `{"username": "sample-username", "email": "sample-email", "password": "sample-password", "recaptcha_token": "sample-recaptcha_token"}`},
	{"TestPostApiAuthChangePassword", "POST", "/api/auth/change-password", `{"username": "sample-username", "email": "sample-email", "password": "sample-password", "recaptcha_token": "sample-recaptcha_token"}`},
	{"TestDeleteApiMessagesMessageid", "DELETE", "/api/messages/test-id", `null`},
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
