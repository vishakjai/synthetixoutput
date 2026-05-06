package main

import (
	"net/http"

	"github.com/go-chi/chi/v5"
)

func registerChatRoutes(r chi.Router) {
	r.Get("/api/chat/{chatId}/messages", getChatMessagesHandler)
	r.Get("/api/chat/{chatId}", getChatHandler)
	r.Get("/api/chat", listChatsHandler)
	r.Post("/api/chat", createChatHandler)
	r.Post("/api/chat/{chatId}/add", addParticipantHandler)
	r.Delete("/api/chat/{chatId}/leave", leaveChatHandler)
	r.Delete("/api/messages/{messageId}", deleteMessageHandler)
	r.Get("/api/participants", listParticipantsHandler)
	r.Post("/api/participants/profile-picture-upload", uploadProfilePictureHandler)
	r.Post("/api/participants/confirm-profile-picture", confirmProfilePictureHandler)
	r.Delete("/api/participants/profile-picture", deleteProfilePictureHandler)
}

func getChatMessagesHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status": "ok"}`))
}

func getChatHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"id": "1", "created_at": "2023-10-10T10:00:00Z", "updated_at": "2023-10-10T10:00:00Z"}`))
}

func listChatsHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"items": [], "total": 0, "page": 1, "page_size": 10}`))
}

func createChatHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusCreated)
	w.Write([]byte(`{"id": "1", "created_at": "2023-10-10T10:00:00Z"}`))
}

func addParticipantHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status": "ok"}`))
}

func leaveChatHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status": "ok"}`))
}

func deleteMessageHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"deleted": true}`))
}

func listParticipantsHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"items": [], "total": 0, "page": 1, "page_size": 10}`))
}

func uploadProfilePictureHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status": "ok"}`))
}

func confirmProfilePictureHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status": "ok"}`))
}

func deleteProfilePictureHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status": "ok"}`))
}
