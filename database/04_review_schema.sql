-- ============================================
-- Review Service Database Schema (Simplified)
-- UTH-ConfMS - Review Management
-- ============================================

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- ============================================
-- REVIEW_ASSIGNMENTS TABLE
-- ============================================
CREATE TABLE review_assignments (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    submission_id UUID NOT NULL,
    reviewer_id UUID NOT NULL,
    assigned_by UUID NOT NULL,
    assigned_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    deadline TIMESTAMP NOT NULL,
    status VARCHAR(20) DEFAULT 'PENDING', -- PENDING, ACCEPTED, DECLINED, COMPLETED
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_review_assignments_submission_id ON review_assignments(submission_id);
CREATE INDEX idx_review_assignments_reviewer_id ON review_assignments(reviewer_id);

-- ============================================
-- REVIEWS TABLE
-- ============================================
CREATE TABLE reviews (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    assignment_id UUID NOT NULL REFERENCES review_assignments(id) ON DELETE CASCADE,
    
    overall_score INT NOT NULL, -- 1-10
    confidence INT NOT NULL, -- 1-5
    recommendation VARCHAR(50) NOT NULL, -- ACCEPT, REJECT, MAJOR_REVISION, MINOR_REVISION
    
    comments TEXT NOT NULL,
    
    submitted_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_reviews_assignment_id ON reviews(assignment_id);

-- ============================================
-- REVIEW_SCORES TABLE
-- ============================================
CREATE TABLE review_scores (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    review_id UUID NOT NULL REFERENCES reviews(id) ON DELETE CASCADE,
    criteria_name VARCHAR(100) NOT NULL, -- Originality, Quality, Clarity
    score INT NOT NULL,
    max_score INT DEFAULT 10,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_review_scores_review_id ON review_scores(review_id);

-- ============================================
-- DECISIONS TABLE
-- ============================================
CREATE TABLE decisions (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    submission_id UUID NOT NULL,
    decision_type VARCHAR(50) NOT NULL, -- ACCEPT, REJECT, MAJOR_REVISION, MINOR_REVISION
    decision_by UUID NOT NULL,
    decision_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    comments TEXT,
    is_final BOOLEAN DEFAULT TRUE
);

CREATE INDEX idx_decisions_submission_id ON decisions(submission_id);

-- ============================================
-- CONFLICTS_OF_INTEREST TABLE
-- ============================================
CREATE TABLE conflicts_of_interest (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    submission_id UUID NOT NULL,
    reviewer_id UUID NOT NULL,
    conflict_type VARCHAR(50) NOT NULL, -- COAUTHOR, ADVISOR, INSTITUTION
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UNIQUE (submission_id, reviewer_id)
);

CREATE INDEX idx_conflicts_submission_id ON conflicts_of_interest(submission_id);
CREATE INDEX idx_conflicts_reviewer_id ON conflicts_of_interest(reviewer_id);

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

CREATE TRIGGER update_reviews_updated_at BEFORE UPDATE ON reviews
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
