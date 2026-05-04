package domain

// NotificationService handles the business logic for notifications.
type NotificationService struct{}

// Execute handles the execution of a notification.
func (s *NotificationService) Execute() string {
	return "Notification executed successfully"
}