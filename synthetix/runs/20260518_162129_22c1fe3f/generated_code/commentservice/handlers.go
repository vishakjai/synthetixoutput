package main

import (
	"encoding/json"
	"net/http"
	"github.com/google/uuid"
)

type RequestPayload struct {
	Payload string `json:"payload"`
}

type ResponsePayload struct {
	Status string `json:"status"`
	ID     string `json:"id"`
}

func commentViewHandler(w http.ResponseWriter, r *http.Request) {
	processRequest(w, r)
}

func createCommentRequestHandler(w http.ResponseWriter, r *http.Request) {
	processRequest(w, r)
}

func tagRepositoryHandler(w http.ResponseWriter, r *http.Request) {
	processRequest(w, r)
}

func globalControllerAdviceHandler(w http.ResponseWriter, r *http.Request) {
	processRequest(w, r)
}

func processRequest(w http.ResponseWriter, r *http.Request) {
	var req RequestPayload
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}

	if req.Payload == "" {
		writeError(w, http.StatusBadRequest, "VALIDATION_ERROR", "payload is required")
		return
	}

	response := ResponsePayload{
		Status: "success",
		ID:     uuid.New().String(),
	}
	writeJSON(w, http.StatusCreated, response)
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
