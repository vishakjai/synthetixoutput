package controllers

import (
	"net/http"
	"notificationmodule/src/Domain"
)

func ExecuteNotification(w http.ResponseWriter, r *http.Request) {
	service := domain.NotificationService{}
	result := service.Execute()
	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"message": "` + result + `"}`))
}