package main

import (
	"errors"
	"time"
)

type Article struct {
	ID        string    `json:"id"`
	CreatedAt time.Time `json:"created_at"`
}

type CreateArticleRequest struct {
	Payload string `json:"payload"`
}

type CreateArticleResponse struct {
	ID        string    `json:"id"`
	CreatedAt time.Time `json:"created_at"`
}

func createArticle(req CreateArticleRequest) (CreateArticleResponse, error) {
	if req.Payload == "" {
		return CreateArticleResponse{}, errors.New("payload cannot be empty")
	}

	article := CreateArticleResponse{
		ID:        "123",
		CreatedAt: time.Now(),
	}

	return article, nil
}

func getArticles() ([]Article, error) {
	// Simulated data
	articles := []Article{
		{ID: "123", CreatedAt: time.Now()},
	}
	return articles, nil
}
