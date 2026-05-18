package main

import (
	"encoding/json"
	"net/http"
)

type RequestPayload struct {
	Payload string `json:"payload"`
}

type ResponsePayload struct {
	Status string `json:"status"`
	ID     string `json:"id"`
}

func commentViewHandler(w http.ResponseWriter, r *http.Request) {
	var req RequestPayload
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}

	// Simulate processing logic
	resp := ResponsePayload{Status: "success", ID: "123"}
	writeJSON(w, http.StatusCreated, resp)
}

func createCommentRequestHandler(w http.ResponseWriter, r *http.Request) {
	var req RequestPayload
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}

	// Simulate processing logic
	resp := ResponsePayload{Status: "success", ID: "124"}
	writeJSON(w, http.StatusCreated, resp)
}

func tagRepositoryHandler(w http.ResponseWriter, r *http.Request) {
	var req RequestPayload
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}

	// Simulate processing logic
	resp := ResponsePayload{Status: "success", ID: "125"}
	writeJSON(w, http.StatusCreated, resp)
}

func offsetBasedPageableHandler(w http.ResponseWriter, r *http.Request) {
	var req RequestPayload
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}

	// Simulate processing logic
	resp := ResponsePayload{Status: "success", ID: "126"}
	writeJSON(w, http.StatusCreated, resp)
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
