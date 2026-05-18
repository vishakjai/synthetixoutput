package main

import (
	"net/http"

	"github.com/go-chi/chi/v5"
)

func userRouter() http.Handler {
	r := chi.NewRouter()

	r.Post("/", signupHandler)
	r.Post("/login", loginHandler)
	r.Get("/", getCurrentUserHandler)
	r.Put("/", updateUserHandler)

	r.Route("/profiles/{username}", func(r chi.Router) {
		r.Get("/", getProfileHandler)
		r.Post("/follow", followHandler)
		r.Delete("/follow", unfollowHandler)
	})

	return r
}

func signupHandler(w http.ResponseWriter, r *http.Request) {
	// Implement signup logic
}

func loginHandler(w http.ResponseWriter, r *http.Request) {
	// Implement login logic
}

func getCurrentUserHandler(w http.ResponseWriter, r *http.Request) {
	// Implement get current user logic
}

func updateUserHandler(w http.ResponseWriter, r *http.Request) {
	// Implement update user logic
}

func getProfileHandler(w http.ResponseWriter, r *http.Request) {
	// Implement get profile logic
}

func followHandler(w http.ResponseWriter, r *http.Request) {
	// Implement follow logic
}

func unfollowHandler(w http.ResponseWriter, r *http.Request) {
	// Implement unfollow logic
}
