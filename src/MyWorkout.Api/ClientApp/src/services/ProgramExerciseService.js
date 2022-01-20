export async function getAllProgramExercisesByProgramIdAsync(programId) {
  const response = await fetch(`/api/programexercise/program/${programId}`);
  return await response.json();
}

export async function createProgramExerciseAsync(programExercise) {
  const response = await fetch(`/api/programexercise`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(programExercise)
  })
  return await response.json();
}

export async function updateProgramExerciseAsync(programExercise) {
  const response = await fetch(`/api/programExercise/${programExercise.id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(programExercise)
  })
  return await response.json();
}

export async function deleteProgramExerciseAsync(programExerciseId) {
  await fetch(`/api/programExercise/${programExerciseId}`, {
    method: 'DELETE',
    headers: { 'Content-Type': 'application/json' }
  })
}
