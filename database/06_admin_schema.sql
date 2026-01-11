-- ============================================
-- Admin Service Database Schema (Simplified)
-- UTH-ConfMS - System Administration
-- ============================================

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- ============================================
-- SYSTEM_SETTINGS TABLE
-- ============================================
CREATE TABLE system_settings (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    category VARCHAR(100) NOT NULL,
    setting_key VARCHAR(150) NOT NULL UNIQUE,
    setting_value TEXT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Insert default settings
INSERT INTO system_settings (category, setting_key, setting_value) VALUES
('EMAIL', 'smtp_host', 'smtp.gmail.com'),
('EMAIL', 'smtp_port', '587'),
('EMAIL', 'from_email', 'noreply@uth-confms.vn'),
('STORAGE', 'storage_path', '/uploads'),
('STORAGE', 'max_file_size_mb', '10');

-- ============================================
-- AUDIT_LOGS TABLE (System-wide)
-- NOTE: This table is already defined in 01_identity_schema.sql
-- Additional indexes for admin queries on existing audit_logs table
-- ============================================
CREATE INDEX IF NOT EXISTS idx_audit_logs_action ON audit_logs(action);
CREATE INDEX IF NOT EXISTS idx_audit_logs_created_at_desc ON audit_logs(created_at DESC);

-- ============================================
-- SYSTEM_LOGS TABLE
-- ============================================
CREATE TABLE system_logs (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    log_level VARCHAR(20) NOT NULL, -- INFO, WARNING, ERROR
    service_name VARCHAR(100) NOT NULL,
    message TEXT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_system_logs_level ON system_logs(log_level);
CREATE INDEX idx_system_logs_service ON system_logs(service_name);
CREATE INDEX idx_system_logs_created_at ON system_logs(created_at DESC);

-- ============================================
-- TRIGGERS
-- ============================================
CREATE OR REPLACE FUNCTION update_updated_at_column()
RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = CURRENT_TIMESTAMP;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER update_system_settings_updated_at BEFORE UPDATE ON system_settings
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
