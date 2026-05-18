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
	resp := CommentResponse{Status: "success", ID: "1"}
	writeJSON(w, http.StatusCreated, resp)
}

func createCommentRequestHandler(w http.ResponseWriter, r *http.Request) {
	var req CommentRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}

	// Simulate processing logic
	resp := CommentResponse{Status: "success", ID: "2"}
	writeJSON(w, http.StatusCreated, resp)
}

func tagRepositoryHandler(w http.ResponseWriter, r *http.Request) {
	var req CommentRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}

	// Simulate processing logic
	resp := CommentResponse{Status: "success", ID: "3"}
	writeJSON(w, http.StatusCreated, resp)
}

func offsetBasedPageableHandler(w http.ResponseWriter, r *http.Request) {
	var req CommentRequest
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}

	// Simulate processing logic
	resp := CommentResponse{Status: "success", ID: "4"}
	writeJSON(w, http.StatusCreated, resp)
}

func writeError(w http.ResponseWriter, status int, code, message string) {
	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(status)
	json.NewEncoder(w).Encode(map[string]string{"code": code, "message": message})
}

func writeJSON(w http.ResponseWriter, status int, payload interface{}) {
	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(status)
	json.NewEncoder(w).Encode(payload)
}