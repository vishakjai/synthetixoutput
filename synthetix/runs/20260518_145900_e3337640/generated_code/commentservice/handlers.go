package main

import (
	"encoding/json"
	"net/http"
)

type CommentRequest struct {
	Payload string `json:"payload"`
}

type CommentResponse struct {
	Status string `json:"status"`
	ID     string `json:"id"`
}

func commentViewHandler(w http.ResponseWriter, r *http.Request) {
	var req CommentRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}

	// Simulate processing logic
	res := CommentResponse{Status: "created", ID: "123"}

	writeJSON(w, http.StatusCreated, res)
}

func createCommentRequestHandler(w http.ResponseWriter, r *http.Request) {
	var req CommentRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}

	// Simulate processing logic
	res := CommentResponse{Status: "created", ID: "456"}

	writeJSON(w, http.StatusCreated, res)
}

func offsetBasedPageableHandler(w http.ResponseWriter, r *http.Request) {
	var req CommentRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}

	// Simulate processing logic
	res := CommentResponse{Status: "created", ID: "789"}

	writeJSON(w, http.StatusCreated, res)
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