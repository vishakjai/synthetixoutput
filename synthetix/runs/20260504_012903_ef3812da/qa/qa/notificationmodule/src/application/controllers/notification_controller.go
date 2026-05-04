package controllers

import (
	"encoding/json"
	"net/http"

	"notificationmodule/src/Domain"
)

func ExecuteNotification(w http.ResponseWriter, r *http.Request) {
	var request struct {
		Message string `json:"message"`
	}

	if err := json.NewDecoder(r.Body).Decode(&request); err != nil {
		http.Error(w, "Invalid request payload", http.StatusBadRequest)
		return
	}

	if request.Message == "" {
		http.Error(w, "Message is required", http.StatusBadRequest)
		return
	}

	service := domain.NotificationService{}
	result := service.Execute(request.Message)

	response := struct {
		Result string `json:"result"`
	}{Result: result}

	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(response)
}