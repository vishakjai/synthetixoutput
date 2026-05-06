package main

type Chat struct {
	ID        string `db:"id"`
	CreatedAt string `db:"created_at"`
	UpdatedAt string `db:"updated_at"`
}

// Define other models as needed.
