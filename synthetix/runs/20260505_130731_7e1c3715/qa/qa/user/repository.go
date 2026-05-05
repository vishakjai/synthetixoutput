package main

import (
	"context"
	"github.com/jmoiron/sqlx"
)

type UserRepository struct {
	db *sqlx.DB
}

func (r *UserRepository) FindUserEventByID(ctx context.Context, id string) (*UserEvent, error) {
	var event UserEvent
	err := r.db.GetContext(ctx, &event, "SELECT id, event_type, timestamp FROM user_events WHERE id = $1", id)
	if err != nil {
		return nil, err
	}
	return &event, nil
}

func (r *UserRepository) CreateUserEvent(ctx context.Context, event UserEvent) error {
	_, err := r.db.ExecContext(ctx, "INSERT INTO user_events (id, event_type, timestamp) VALUES ($1, $2, $3)", event.ID, event.EventType, event.Timestamp)
	return err
}
