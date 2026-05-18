package main

import (
	"encoding/json"
	"net/http"
)

type Response struct {
	Status string `json:"status"`
	ID     string `json:"id"`
}

type Request struct {
	Payload string `json:"payload"`
}

func commentViewHandler(w http.ResponseWriter, r *http.Request) {
	var req Request
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}

	// Simulate processing logic
	res := Response{Status: "success", ID: "123"}
	writeJSON(w, http.StatusCreated, res)
}

func createCommentRequestHandler(w http.ResponseWriter, r *http.Request) {
	var req Request
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}

	// Simulate processing logic
	res := Response{Status: "success", ID: "456"}
	writeJSON(w, http.StatusCreated, res)
}

func tagRepositoryHandler(w http.ResponseWriter, r *http.Request) {
	var req Request
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}

	// Simulate processing logic
	res := Response{Status: "success", ID: "789"}
	writeJSON(w, http.StatusCreated, res)
}

func globalControllerAdviceHandler(w http.ResponseWriter, r *http.Request) {
	var req Request
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}

	// Simulate processing logic
	res := Response{Status: "success", ID: "101"}
	writeJSON(w, http.StatusCreated, res)
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
