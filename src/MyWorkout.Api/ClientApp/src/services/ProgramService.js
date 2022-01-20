export async function getAllProgramsAsync() {
  const response = await fetch('/api/program');
  return await response.json();
}

export async function createProgramAsync(program) {
  const response = await fetch(`/api/program`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(program)
  })
  return await response.json();
}

export async function updateProgramAsync(program) {
  const response = await fetch(`/api/program/${program.id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(program)
  })
  return await response.json();
}

export async function deleteProgramAsync(programId) {
  await fetch(`/api/program/${programId}`, {
    method: 'DELETE',
    headers: { 'Content-Type': 'application/json' }
  })
}
