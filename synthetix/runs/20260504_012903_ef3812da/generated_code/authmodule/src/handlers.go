package main

import "net/http"

func ConfirmProfilePictureRequestHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status": "ConfirmProfilePictureRequest executed"}`))
}

func PictureUploadResponseHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status": "PictureUploadResponse executed"}`))
}

func DeleteMessageDtoHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status": "DeleteMessageDto executed"}`))
}
