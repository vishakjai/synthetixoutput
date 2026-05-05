package main

import (
	"context"

	"github.com/jmoiron/sqlx"
)

type UserEventRepository struct {
	db *sqlx.DB
}

func (r *UserEventRepository) CreateUserEvent(ctx context.Context, event UserEvent) error {
	_, err := r.db.ExecContext(ctx, `INSERT INTO user_events (id, type, data) VALUES ($1, $2, $3)`, event.ID, event.Type, event.Data)
	return err
}

func (r *UserEventRepository) GetUserEventByID(ctx context.Context, id string) (*UserEvent, error) {
	var event UserEvent
	err := r.db.GetContext(ctx, &event, `SELECT id, type, data FROM user_events WHERE id = $1`, id)
	if err != nil {
		return nil, err
	}
	return &event, nil
}

type UserEvent struct {
	ID   string `db:"id"`
	Type string `db:"type"`
	Data string `db:"data"`
}