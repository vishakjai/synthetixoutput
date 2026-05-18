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

func profileViewHandler(w http.ResponseWriter, r *http.Request) {
	var req Request
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}

	if req.Payload == "" {
		writeError(w, http.StatusBadRequest, "VALIDATION_ERROR", "payload required")
		return
	}

	resp := Response{Status: "success", ID: "profile-view-id"}
	writeJSON(w, http.StatusCreated, resp)
}

func localeConfigurerHandler(w http.ResponseWriter, r *http.Request) {
	var req Request
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}

	if req.Payload == "" {
		writeError(w, http.StatusBadRequest, "VALIDATION_ERROR", "payload required")
		return
	}

	resp := Response{Status: "success", ID: "locale-configurer-id"}
	writeJSON(w, http.StatusCreated, resp)
}

func notBlankOrNullHandler(w http.ResponseWriter, r *http.Request) {
	var req Request
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeError(w, http.StatusBadRequest, "INVALID_PAYLOAD", err.Error())
		return
	}

	if req.Payload == "" {
		writeError(w, http.StatusBadRequest, "VALIDATION_ERROR", "payload required")
		return
	}

	resp := Response{Status: "success", ID: "not-blank-or-null-id"}
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
