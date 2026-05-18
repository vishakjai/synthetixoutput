package main

import "time"

type Article struct {
	ID        string    `json:"id"`
	CreatedAt time.Time `json:"created_at"`
	UpdatedAt time.Time `json:"updated_at"`
}

type Comment struct {
	ID        string    `json:"id"`
	ArticleID string    `json:"article_id"`
	Content   string    `json:"content"`
	CreatedAt time.Time `json:"created_at"`
}

type Tag struct {
	ID   string `json:"id"`
	Name string `json:"name"`
}
