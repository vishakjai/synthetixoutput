package main

import (
	"net/http"
	"net/http/httptest"
	"testing"

	"github.com/gin-gonic/gin"
	"github.com/stretchr/testify/assert"
)

func TestHealthEndpoint(t *testing.T) {
	gin.SetMode(gin.TestMode)
	r := gin.Default()
	r.GET("/health", func(c *gin.Context) {
		c.JSON(http.StatusOK, gin.H{"status": "healthy"})
	})

	w := httptest.NewRecorder()
	req, _ := http.NewRequest(http.MethodGet, "/health", nil)
	r.ServeHTTP(w, req)

	assert.Equal(t, http.StatusOK, w.Code)
	assert.JSONEq(t, `{"status": "healthy"}`, w.Body.String())
}

func TestReadyEndpoint(t *testing.T) {
	gin.SetMode(gin.TestMode)
	r := gin.Default()
	r.GET("/ready", func(c *gin.Context) {
		c.JSON(http.StatusOK, gin.H{"status": "ready"})
	})

	w := httptest.NewRecorder()
	req, _ := http.NewRequest(http.MethodGet, "/ready", nil)
	r.ServeHTTP(w, req)

	assert.Equal(t, http.StatusOK, w.Code)
	assert.JSONEq(t, `{"status": "ready"}`, w.Body.String())
}
