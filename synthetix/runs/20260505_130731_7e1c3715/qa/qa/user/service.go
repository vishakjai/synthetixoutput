package main

import (
	"context"
	"errors"
	"time"
)

type UserService struct {
	repo UserRepository
}

func (s *UserService) Register(ctx context.Context, username, email, password string) (SignupResponse, error) {
	if username == "" || email == "" || password == "" {
		return SignupResponse{}, errors.New("missing required fields")
	}
	userID := "123e4567-e89b-12d3-a456-426614174000" // Simulated user ID
	createdAt := time.Now().Format(time.RFC3339)
	return SignupResponse{UserID: userID, Username: username, CreatedAt: createdAt}, nil
}

func (s *UserService) Login(ctx context.Context, username, password string) (JwtAuthResponse, error) {
	if username != "testuser" || password != "password" {
		return JwtAuthResponse{}, errors.New("invalid credentials")
	}
	token, err := signJWT("123", []string{"user"}, []byte("secret"), 24*time.Hour)
	if err != nil {
		return JwtAuthResponse{}, err
	}
	return JwtAuthResponse{
		Token:      token,
		TokenType:  "Bearer",
		Username:   username,
		Authorities: []string{"user"},
		ExpiresAt:  time.Now().Add(24 * time.Hour).Format(time.RFC3339),
	}, nil
}
