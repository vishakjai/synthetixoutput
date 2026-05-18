package main

import (
	"encoding/json"
	"net/http"
)

func createArticleHandler(w http.ResponseWriter, r *http.Request) {
	// Decode request body
	var req CreateArticleRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}

	// Validate required fields
	if req.Payload == "" {
		writeError(w, http.StatusBadRequest, "VALIDATION_ERROR", "payload required")
		return
	}

	// Call service method
	article, err := createArticle(req)
	if err != nil {
		writeError(w, http.StatusInternalServerError, "INTERNAL_ERROR", err.Error())
		return
	}

	// Respond with created article
	writeJSON(w, http.StatusCreated, article)
}

func getArticlesHandler(w http.ResponseWriter, r *http.Request) {
	articles, err := getArticles()
	if err != nil {
		writeError(w, http.StatusInternalServerError, "INTERNAL_ERROR", err.Error())
		return
	}

	writeJSON(w, http.StatusOK, articles)
}

func feedHandler(w http.ResponseWriter, r *http.Request) {
	writeJSON(w, http.StatusOK, map[string]string{"status": "ok"})
}

func getArticleHandler(w http.ResponseWriter, r *http.Request) {
	// Implementation here
}

func updateArticleHandler(w http.ResponseWriter, r *http.Request) {
	// Implementation here
}

func deleteArticleHandler(w http.ResponseWriter, r *http.Request) {
	// Implementation here
}

func favoriteArticleHandler(w http.ResponseWriter, r *http.Request) {
	// Implementation here
}

func unfavoriteArticleHandler(w http.ResponseWriter, r *http.Request) {
	// Implementation here
}

func getCommentsHandler(w http.ResponseWriter, r *http.Request) {
	// Implementation here
}

func addCommentHandler(w http.ResponseWriter, r *http.Request) {
	// Implementation here
}

func deleteCommentHandler(w http.ResponseWriter, r *http.Request) {
	// Implementation here
}

func getTagsHandler(w http.ResponseWriter, r *http.Request) {
	// Implementation here
}

func writeError(w http.ResponseWriter, status int, code, message string) {
	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(status)
	json.NewEncoder(w).Encode(map[string]string{"code": code, "message": message})
}

func writeJSON(w http.ResponseWriter, status int, payload any) {
	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(status)
	json.NewEncoder(w).Encode(payload)
}
