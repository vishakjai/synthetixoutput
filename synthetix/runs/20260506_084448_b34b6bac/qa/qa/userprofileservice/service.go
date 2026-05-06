package main

import (
	"context"
	"errors"
	"time"

	"github.com/golang-jwt/jwt/v5"
	"golang.org/x/crypto/bcrypt"
)

var jwtSecret = []byte("your-256-bit-secret")

func hashPassword(password string) (string, error) {
	bytes, err := bcrypt.GenerateFromPassword([]byte(password), 14)
	return string(bytes), err
}

func checkPasswordHash(password, hash string) bool {
	err := bcrypt.CompareHashAndPassword([]byte(hash), []byte(password))
	return err == nil
}

func signJWT(userID string, roles []string, secret []byte, ttl time.Duration) (string, error) {
	claims := jwt.MapClaims{
		"sub":  userID,
		"roles": roles,
		"exp":  time.Now().Add(ttl).Unix(),
		"iat":  time.Now().Unix(),
	}
	token := jwt.NewWithClaims(jwt.SigningMethodHS256, claims)
	return token.SignedString(secret)
}

func authenticateUser(ctx context.Context, username, password string) (string, error) {
	// Simulate user authentication
	if username == "user" && password == "pass" {
		return "123e4567-e89b-12d3-a456-426614174000", nil
	}
	return "", errors.New("invalid credentials")
}
